using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WLBusinessLogic.Interfaces;
using WLDataLayer.DAL.Interfaces;
using WLDataLayer.Identity;

namespace WLBusinessLogic.Managers
{
    public class WordManager : BaseManager, IWordManager
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public WordManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(unitOfWork, mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


    }
}
