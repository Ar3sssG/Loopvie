using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WLBusinessLogic.Interfaces;
using WLDataLayer.DAL.Interfaces;
using WLDataLayer.DAL.StoreServices;
using WLDataLayer.Identity;

namespace WLBusinessLogic.Managers
{
    public class WordManager : BaseManager, IWordManager
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly WordStoreService _wordStoreService;
        private readonly AnswerStoreService _answerStoreService;
        public WordManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager,WordStoreService wordStoreService, AnswerStoreService answerStoreService) : base(unitOfWork, mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _wordStoreService = wordStoreService;
            _answerStoreService = answerStoreService;
        }


    }
}
