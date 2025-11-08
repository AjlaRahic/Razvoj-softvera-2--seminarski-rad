using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFood.Services.Reports
{
    public interface IReportService
    {
        List<UplatePoKorisniku> ReportUplatePoKorisniku();
        List<PrometPoKorisniku> ReportPrometPoKorisniku();


    }
}
