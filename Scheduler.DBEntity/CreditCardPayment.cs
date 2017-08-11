using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.DBEntity
{
    public class CreditCardPayment : EntityType
    {
        public string PayerName { get; set; }
        public AddressType BillingAddress { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardExpirationYear { get; set; }
        public string CardCVV { get; set; }

        public CreditCardPayment() { }

        //public static CreditCardPaymentDto Convert2Dto(CreditCardPayment type)
        //{
        //    CreditCardPaymentDto r = new CreditCardPaymentDto();
        //    r.BillingAddress = AddressType.Convert2Dto(type.BillingAddress);
        //    r.CardCVV = type.CardCVV;
        //    r.CardExpirationMonth = type.CardExpirationMonth;
        //    r.CardExpirationYear = type.CardExpirationYear;
        //    r.CardNumber = type.CardNumber;
        //    r.CardType = type.CardType;
        //    r.PayerName = type.PayerName;
        //    return r;
        //}

        //internal static CreditCardPayment ExtractFromDto(CreditCardPaymentDto type)
        //{
        //    CreditCardPayment r = new CreditCardPayment();
        //    r.BillingAddress = AddressType.ExtractFromDto(type.BillingAddress);
        //    r.CardCVV = type.CardCVV;
        //    r.CardExpirationMonth = type.CardExpirationMonth;
        //    r.CardExpirationYear = type.CardExpirationYear;
        //    r.CardNumber = type.CardNumber;
        //    r.CardType = type.CardType;
        //    r.PayerName = type.PayerName;
        //    return r;
        //}

    }

    public class ChequePayment : EntityType
    {
        public string PayerName { get; set; }
        public string ChequeBankName { get; set; }
        public string ChequeRoutingNumber { get; set; }
        public string ChequeAccountNumber { get; set; }
        public string ChequeNumber { get; set; }
        public int ImagesCount { get; set; }
        public DateTime ChequeDate { get; set; }
        public List<SchedulerImage> ChequePictures { get; set; }

        public ChequePayment() { ChequePictures = new List<SchedulerImage>(); }

        //public static ChequePaymentDto Convert2Dto(ChequePayment type)
        //{
        //    ChequePaymentDto r = new ChequePaymentDto();
        //    r.ChequeBankName = type.ChequeBankName;
        //    r.ImagesCount = type.ImagesCount;
        //    r.ChequeRoutingNumber = type.ChequeRoutingNumber;
        //    r.ChequeAccountNumber = type.ChequeAccountNumber;
        //    r.ChequeNumber = type.ChequeNumber;
        //    r.ChequeDate = type.ChequeDate;
        //    r.PayerName = type.PayerName;

        //    foreach (SchedulerImage image in type.ChequePictures)
        //        r.ChequePictures.Add(SchedulerImage.Convert2Dto(image));

        //    return r;
        //}

        //internal static ChequePayment ExtractFromDto(ChequePaymentDto type)
        //{
        //    ChequePayment r = new ChequePayment();
        //    r.ChequeBankName = type.ChequeBankName;
        //    r.ChequeRoutingNumber = type.ChequeRoutingNumber;
        //    r.ChequeAccountNumber = type.ChequeAccountNumber;
        //    r.ChequeNumber = type.ChequeNumber;
        //    r.ChequeDate = type.ChequeDate;
        //    r.PayerName = type.PayerName;

        //    foreach (ImageDto dto in type.ChequePictures)
        //        r.ChequePictures.Add(SchedulerImage.ExtractFromDto(dto));

        //    return r;
        //}
    }
}
