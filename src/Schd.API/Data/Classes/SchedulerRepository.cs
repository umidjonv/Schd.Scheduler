using Microsoft.EntityFrameworkCore;
using Schd.API.Data.Interfaces;
using Schd.API.Models;
using Schd.Data.Entity;
using Schd.Data.Enums;
using Schd.Scheduler.Data;
using Schd.Scheduler.Data.Entity.Schedules;

namespace Schd.API.Data.Classes
{
    public class SchedulerRepository : ICrudeRepository<SchedulerTemplateViewModel>
    {
        private readonly IAppDbContext _db;

        public SchedulerRepository(IAppDbContext db)
        {
            _db = db;
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    _db.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<List<SchedulerTemplateViewModel>> GetAllAsync()
        {
            var entity = await _db.ScheduleTemplates.ToListAsync();
            if (entity.Count < 1)
                return new List<SchedulerTemplateViewModel>();

            var result = entity.Select(s => new SchedulerTemplateViewModel
            {
                Id = s.Id,
                Checked = s.Enabled,
                Name = s.Name,
                RegularityDaysEnum = (int)s.RegularityDays,
                RegularityTimesEnum = (int)s.RegularityHours,
                RegularityDays = s.CustomDates,
                RegularityTimes = s.CustomTimes
            }).ToList();


            return result;
        }

        public async Task<SchedulerTemplateViewModel> GetByIdAsync(long id)
        {
            var model = await _db.ScheduleTemplates.FirstOrDefaultAsync(s => s.Id == id);
            if (model == null)
                return new SchedulerTemplateViewModel();

            var result = new SchedulerTemplateViewModel();
            result.Id = model.Id;
            result.Checked = model.Enabled;
            result.Name = model.Name;
            result.RegularityDaysEnum = (int)model.RegularityDays;
            result.RegularityTimesEnum = (int)model.RegularityHours;
            result.RegularityDays = model.CustomDates;
            result.RegularityTimes = model.CustomTimes;

            return result;
        }

        public async Task CreateAsync(SchedulerTemplateViewModel item)
        {
            if (item != null)
            {
                var model = new ScheduleTemplate();
                model.Name = item.Name;
                model.RegularityHours = (RegularityHours)item.RegularityTimesEnum;
                model.RegularityDays = (RegularityDays)item.RegularityDaysEnum;
                model.CustomDates = item.RegularityDays;
                model.CustomTimes = item.RegularityTimes;
                model.Enabled = item.Checked;

                await _db.ScheduleTemplates.AddAsync(model);
            }
            await SaveAsync();
        }

        public async Task UpdateAsync(SchedulerTemplateViewModel item)
        {
            if (item != null)
            {
                var entity = await _db.ScheduleTemplates.FirstOrDefaultAsync(s => s.Id == item.Id);
                if (entity != null)
                {
                    entity.Enabled = item.Checked;
                    entity.Name = item.Name;
                    entity.RegularityDays = (RegularityDays)item.RegularityDaysEnum;
                    entity.RegularityHours = (RegularityHours)item.RegularityTimesEnum;
                    entity.CustomDates = item.RegularityDays;
                    entity.CustomTimes = item.RegularityTimes;
                    _db.ScheduleTemplates.Update(entity);
                    await SaveAsync();
                }
            }
        }

        public void DeleteById(long id)
        {
            var scheduleTemplates = _db.ScheduleTemplates.FirstOrDefault(s => s.Id == id);
            if (scheduleTemplates != null)
            {
                _db.ScheduleTemplates.Remove(scheduleTemplates);
                Save();
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
        
        public async Task ActiveAsync(long id)
        {
            var entity = await _db.ScheduleTemplates.Where(s=>s.Id==id).FirstOrDefaultAsync();
            entity.Enabled = !entity.Enabled;
            _db.ScheduleTemplates.Update(entity);
            await SaveAsync();
            return;
        } 
    }
}
