using FitComrade.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FitComrade.Core.Services
{
    public interface IBlogService
    {
        Blog CreateBlog(ISession session);
        Task AddWorkoutToBlogAsync(int id, Workout workout);
    }
}
