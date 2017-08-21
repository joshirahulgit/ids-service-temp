using Scheduler.Business.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduler.Business.Entity;
using Scheduler.Core;

namespace Scheduler.Business.Implementation
{
    public class CptService : ServiceBase, ICptService
    {
        public void AddDiagnosisToLocalStorage(DiagnosisDto diag)
        {
            throw new NotImplementedException();
        }

        public void AddProcedureToLocalStorage(ProcedureDto proc)
        {
            throw new NotImplementedException();
        }

        public string CreateOrder(OrderCreateParametersDto orderCreateParameters)
        {
            throw new NotImplementedException();
        }

        public DiagnosesDto GetDiagnosisSuggestionList(string searchString, int category, CPTCodeSearchMode mode)
        {
            throw new NotImplementedException();
        }

        public ProceduresDto GetDiagnosisToProcedureHintList(DiagnosisDto diagnosis)
        {
            throw new NotImplementedException();
        }

        public DiagnosesDto GetLocalDiagnosesAdminList()
        {
            throw new NotImplementedException();
        }

        public ProceduresDto GetLocalProcedureAdminList()
        {
            throw new NotImplementedException();
        }

        public ModifiersDto GetModifierSuggestionList(string searchString)
        {
            throw new NotImplementedException();
        }

        public ProceduresDto GetProceduresSuggestionList(List<string> searchString, int category, CPTCodeSearchMode mode, bool exactMatch)
        {
            throw new NotImplementedException();
        }

        public ProceduresDto GetProcedureSuggestionList(string searchString, int category, CPTCodeSearchMode mode, bool exactMatch)
        {
            throw new NotImplementedException();
        }

        public ProceduresDto GetProcedureSuggestionListWithRoom(string searchString, int category, CPTCodeSearchMode mode, bool exactMatch, long? roomId)
        {
            throw new NotImplementedException();
        }

        public DiagnosesDto GetProcedureToDiagnosisHintList(ProcedureDto procedure)
        {
            throw new NotImplementedException();
        }

        public SnomedsDto GetSnomedSuggestionList(string searchString)
        {
            throw new NotImplementedException();
        }

        public bool HasDiagnosisToProcedureHintList(DiagnosisDto diagnosis)
        {
            throw new NotImplementedException();
        }

        public bool HasProcedureToDiagnosisHintList(ProcedureDto procedure)
        {
            throw new NotImplementedException();
        }

        public void UpdateDiagnosesToLocalStorage(DiagnosesDto diags, DiagnosesDto deleted)
        {
            throw new NotImplementedException();
        }

        public AppointmentOrderDto UpdateOrder(AppointmentOrderDto order)
        {
            throw new NotImplementedException();
        }

        public void UpdateProceduresToLocalStorage(ProceduresDto procs, ProceduresDto deleted)
        {
            throw new NotImplementedException();
        }
    }
}
