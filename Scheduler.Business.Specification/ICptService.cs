using Scheduler.Business.Entity;
using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Business.Specification
{
    public interface ICptService : IContract
    {

        SnomedsDto GetSnomedSuggestionList(String searchString);

        DiagnosesDto GetDiagnosisSuggestionList(string searchString, int category, CPTCodeSearchMode mode);

        ProceduresDto GetProcedureSuggestionList(string searchString, int category, CPTCodeSearchMode mode, bool exactMatch);

        ProceduresDto GetProcedureSuggestionListWithRoom(string searchString, int category, CPTCodeSearchMode mode, bool exactMatch, long? roomId);

        ProceduresDto GetLocalProcedureAdminList();

        DiagnosesDto GetLocalDiagnosesAdminList();

        ProceduresDto GetProceduresSuggestionList(List<string> searchString, int category, CPTCodeSearchMode mode, bool exactMatch);

        ModifiersDto GetModifierSuggestionList(string searchString);

        bool HasProcedureToDiagnosisHintList(ProcedureDto procedure);

        bool HasDiagnosisToProcedureHintList(DiagnosisDto diagnosis);

        DiagnosesDto GetProcedureToDiagnosisHintList(ProcedureDto procedure);

        ProceduresDto GetDiagnosisToProcedureHintList(DiagnosisDto diagnosis);

        String CreateOrder(OrderCreateParametersDto orderCreateParameters);

        AppointmentOrderDto UpdateOrder(AppointmentOrderDto order);

        void AddProcedureToLocalStorage(ProcedureDto proc);

        void AddDiagnosisToLocalStorage(DiagnosisDto diag);

        void UpdateProceduresToLocalStorage(ProceduresDto procs, ProceduresDto deleted);

        void UpdateDiagnosesToLocalStorage(DiagnosesDto diags, DiagnosesDto deleted);

    }
}
