using Advert.Dal.UnitOfWorks;
using Advert.Entity.Entities;
using Microsoft.AspNetCore.Mvc.Filters;

public class AdvertVisitorFilter : IAsyncActionFilter
{
    private readonly IUnitOfWork _unitOfWork;

    public AdvertVisitorFilter(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        List<VisitorsEntity> visitors = _unitOfWork.GetRepository<VisitorsEntity>().GetAllAsync().Result;


        string getIp = context.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        string getUserAgent = context.HttpContext.Request.Headers["User-Agent"];

        VisitorsEntity visitor = new(getIp, getUserAgent);



        if (visitors.Any(x => x.IpAddress == visitor.IpAddress))
            return next();
        else
        {
            _unitOfWork.GetRepository<VisitorsEntity>().AddAsync(visitor);
            _unitOfWork.Save();
        }
        return next();
    }
}

