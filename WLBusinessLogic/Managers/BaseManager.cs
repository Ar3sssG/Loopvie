using AutoMapper;
using WLBusinessLogic.Interfaces;
using WLDataLayer.DAL.Interfaces;

namespace WLBusinessLogic.Managers
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
