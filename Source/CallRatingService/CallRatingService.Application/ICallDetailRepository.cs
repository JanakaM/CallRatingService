using CallRatingService.Model;

namespace CallRatingService.Application
{
    public interface ICallDetailRepository
    {
        int SaveCallDetail(CallDetail callDetail);
    }
}
