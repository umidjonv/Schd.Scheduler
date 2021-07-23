using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitProject.Models;
using RabbitProject.Rabbit.Enums;

namespace RabbitProject.Rabbit
{
    public interface IRabbitClient
    {
        void CreateModel();

        void CloseConnection();

        void Publish<T>(T model)
            where T: IBaseModel;

        void Publish<T>(T model, RouteType type)
            where T : IBaseModel;

    }

    
}
