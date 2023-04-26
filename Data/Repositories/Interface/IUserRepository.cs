using Data.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interface
{
    public interface IUserRepository
    {
        public Task<ReponseObject> AddUser(AddUserDTO addUserDto);
        public Task<ReponseObject> Delete(string Id);
		Task<User> GetById(string Id);
        public Task<ReponseObject> Update(string id, UpdateUserDTO updateUser);
        public Task<IEnumerable<User>> GetAllUser(int page, int perpage);
        //public Task<User> UpdateImage(string id, UpdateImageDTO updateImage);
        public Task<IEnumerable<User>> SearchByName(string name, int page, int perpage);
      //  public Task<ReponseObject<object>> Login(LoginDTOs loginDTOs);
        public Task<ReponseObject> AddRole(RoleDTOs roleDTOs);

	}
}
