using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WLBusinessLogic.Interfaces;
using WLBusinessLogic.Managers;
using WLCommon.Constants;
using WLCommon.Models.Manager;
using WLDataLayer.DAL.Entities;
using WLDataLayer.DAL.Interfaces;
using WLDataLayer.Identity;

namespace WLBusinessLogic.Managers
{
    public class IdentityManager : BaseManager, IIdentityManager
    {
        public IdentityManager(IUnitOfWork unitOfWork,IMapper mapper) : base(unitOfWork,mapper)
        {
        }

        public async Task AddRefreshToken(RefreshTokenModel model)
        {
            //check if user have token delete
            var oldToken = await _unitOfWork.RefreshTokenRepository.Get(x => x.UserId == model.UserId).FirstOrDefaultAsync();
            if (oldToken != null)
            {
                _unitOfWork.RefreshTokenRepository.Delete(oldToken);
            }

            RefreshToken refreshToken = new RefreshToken
            {
                UserId = model.UserId,
                Token = model.Token,
                ExpireDate = model.ExpireDate,
                CreationDate = DateTime.UtcNow,
            };

            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            var token = await _unitOfWork.RefreshTokenRepository.Get(x => x.Token == refreshToken).Include(x => x.User).FirstOrDefaultAsync();

            if (token == null)
            {
                throw new Exception(AuthErrorConstants.InvalidRefreshToken);
            }

            if (token.ExpireDate < DateTime.UtcNow)
            {
                throw new Exception(AuthErrorConstants.ExpiredRefreshToken);
            }

            User user = token.User;

            return user;
        }
    }
}
