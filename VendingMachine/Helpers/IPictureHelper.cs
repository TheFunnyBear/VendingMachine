using System.Web;
using VendingMachine.Model;

namespace VendingMachine.Helpers
{
    public interface IPictureHelper
    {
        Picture GetPictureFromPostedFile(HttpPostedFileBase httpPostedFile, PictureType pictureType);
    }
}