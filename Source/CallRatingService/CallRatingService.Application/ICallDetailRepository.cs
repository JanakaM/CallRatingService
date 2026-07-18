using CallRatingService.Model;

namespace CallRatingService.Application
{
    public interface ICallDetailRepository
    {
        Task<int> SaveCallDetail(List<CallDetail> callDetail);
    }
}
