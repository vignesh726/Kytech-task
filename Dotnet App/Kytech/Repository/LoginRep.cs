using Kytech.models;
using Microsoft.EntityFrameworkCore;

namespace Kytech.Repository;

     public class LoginRep: ILogin
    {
        private KytechContext _kytechContext;
        public LoginRep(KytechContext kytechContext)
        {
        _kytechContext=kytechContext;
        }

    public async Task<Login> AddUser(Login user)
    {
            var result=  await _kytechContext.Logins.AddAsync(user);                
            await this._kytechContext.SaveChangesAsync();
            return result.Entity;
    }
    public async Task<bool> DeleteUser(string UserName)
    {        
            var result = await _kytechContext.Logins.FirstOrDefaultAsync(x => x.Username == UserName);
            if(result!=null){
                this._kytechContext.Logins.Remove(result);
                this._kytechContext.SaveChanges();   
                return true;
            }else{
                return false;
            }     
    }
    
    public async Task<Login> GetUserById(string user)
    {
        var res=await _kytechContext.Logins.FirstOrDefaultAsync(x => x.Username == user);
        if(res==null){
           res= new Login();
        }
       return res;
        
    }

   
}
