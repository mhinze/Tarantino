
using Tarantino.Core.WebManagement.Model;

namespace Tarantino.Core.WebManagement.Services
{
    
    public interface IApplicationListingRowView
    {
        string BuildFirstRowHtml(ApplicationInstance applicationInstance, int instanceCount);
        string BuildMRowHtml(ApplicationInstance applicationInstance);
    }
}