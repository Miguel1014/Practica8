using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace Practica8

{
    public interface ISQLAzure
    {
        Task<MobileServiceUser> Authenticate();




    }

   


}