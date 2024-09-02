using AutoMapper;
using LoopvieBusinessLogic.Interfaces;
using LoopvieDataLayer.DAL.Interfaces;

namespace LoopvieBusinessLogic.Managers
{
    public class BaseManager : IBaseManager
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public BaseManager(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
