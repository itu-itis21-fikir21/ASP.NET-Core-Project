using Core.Entities;
using DataAccess.UnitOfWorks;
using Entity.Entities;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyApp.Filters.ArticleVisitors
{
    public class ArticleVisitorFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork unitOfWork;

        public ArticleVisitorFilter(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

       

        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            
            List<Visitor> visitors = unitOfWork.GetRepository<Visitor>().GetAll().Result;


            string getIp = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            string getUserAgent = context.HttpContext.Request.Headers["User-Agent"];

            Visitor visitor = new(getIp, getUserAgent);



            if (visitors.Any(x => x.IpAddress == visitor.IpAddress))
                return next();
            else
            {
                unitOfWork.GetRepository<Visitor>().Add(visitor);
                unitOfWork.Save();
            }
            return next();

        }
    }
}
