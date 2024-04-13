using Kytech.models;
using Microsoft.EntityFrameworkCore;

namespace Kytech.Repository;

     public class Registration: IRegistration
    {
        private KytechContext _kytechContext;
        public Registration(KytechContext kytechContext){
        _kytechContext=kytechContext;
        }

    public async Task<UserDetail> AddUser(UserDetail User)
    {
            var result=  await _kytechContext.UserDetails.AddAsync(User);                
           await this._kytechContext.SaveChangesAsync();
            return result.Entity;
    }
    public async Task<bool> DeleteUser(int id)
    {        
            var result = await _kytechContext.UserDetails.FirstOrDefaultAsync(x => x.Id == id);
            if(result!=null){
                this._kytechContext.UserDetails.Remove(result);
                this._kytechContext.SaveChanges();   
                return true;
            }else{
                return false;
            }     
    }
    public async Task<UserDetail> GetUserById(int id)
    {
        var res=await _kytechContext.UserDetails.FirstOrDefaultAsync(x => x.Id == id);
        if(res==null){
           res= new UserDetail();
        }
       return res;
        
    }
    public async Task<IEnumerable<UserDetail>> GetAllUsers()
    {
            return await this._kytechContext.UserDetails.ToListAsync();
    }
   public async Task<UserDetail> UpdateUser(UserDetail User, int id)
    {
        var result=   await _kytechContext.UserDetails.FirstOrDefaultAsync(x => x.Id == id);

            if(result != null)
            {
                result.Address = User.Address;
                result.Phone = User.Phone;
                result.Firstname=User.Firstname;
                result.Lastname=User.Lastname;
                this._kytechContext.SaveChanges();

            }
            return result;
    }
}
