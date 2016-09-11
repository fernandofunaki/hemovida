using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.HealthChecker
{
    public interface IHealthCheckerInfoCaptureRepository
    {
        void Salvar(HealthCheckerInfoCapture contato);
        void Excluir(HealthCheckerInfoCapture contato);
        void Alterar(HealthCheckerInfoCapture contato);
        HealthCheckerInfoCapture ObterPor(int id);
    }
}
