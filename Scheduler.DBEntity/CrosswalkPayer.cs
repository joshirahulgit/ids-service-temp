using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class CrosswalkPayer : EntityBase
    {
        public int Id { get; set; }
        public CustomPayer LocalPayer { get; set; }
        public CustomPayer GlobalPayer { get; set; }

        public int LocalPayerId { get; set; }
        public int GlobalPayerId { get; set; }

        public CrosswalkPayer(int id, int localPayerId, int globalPayerId)
        {
            Id = id;
            LocalPayerId = localPayerId;
            GlobalPayerId = globalPayerId;
        }

        public CrosswalkPayer(int id, CustomPayer localPayer, CustomPayer globalPayer)
        {
            Id = id;
            LocalPayer = localPayer;
            GlobalPayer = globalPayer;
        }

        //internal static CrosswalkPayerDto Convert2Dto(CrosswalkPayer payer)
        //{
        //    CrosswalkPayerDto r = new CrosswalkPayerDto();
        //    r.Id = payer.Id;
        //    r.LocalPayer = CustomPayer.Convert2Dto(payer.LocalPayer);
        //    r.GlobalPayer = CustomPayer.Convert2Dto(payer.GlobalPayer);
        //    return r;
        //}


        //internal static CrosswalkPayer ExtractFromDto(CrosswalkPayerDto payer)
        //{
        //    CrosswalkPayer r = new CrosswalkPayer(payer.Id, CustomPayer.ExtractFromDto(payer.LocalPayer),
        //                                          CustomPayer.ExtractFromDto(payer.GlobalPayer));
        //    return r;
        //}

        public void SetPayers(CustomPayer local, CustomPayer globalPayer)
        {
            LocalPayer = local;
            GlobalPayer = globalPayer;
        }
    }
}
