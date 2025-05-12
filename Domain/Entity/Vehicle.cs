using System.Text.RegularExpressions;
using cp_02.Domain.Interfaces;

namespace cp_02.Domain.Entity
{
    public class Vehicle : ICancel
    {
        #region ICancel Properties
        public Guid UserCancelID { get; set; }
        public bool IsCancel { get; set; }
        #endregion

        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string VehicleModel { get; set; }

        public Vehicle(Guid id, string licensePlate, string vehicleModel)
        {
            Id = id;
            LicensePlate = VerifyLicensePlate(licensePlate);
            VehicleModel = vehicleModel;

            IsCancel = false;
            UserCancelID = Guid.Empty;
        }

        private string VerifyLicensePlate(string licensePlate)
        {
            licensePlate = licensePlate.Replace(" ", "").ToUpper();
            if (Regex.IsMatch(licensePlate, @"^[A-Z]{3}\d{4}$") ||
                Regex.IsMatch(licensePlate, @"^[A-Z]{3}\d[A-Z]\d{2}$"))
            {
                return licensePlate;
            }
            throw new ArgumentException("O formato da placa está inválido. Tente novamente com o formato: AAA1234 ou AAA1A23");
        }

        public void Cancel(Guid userID)
        {
            if (!IsCancel)
            {
                IsCancel = true;
                UserCancelID = userID;
            }
            else
            {
                throw new InvalidOperationException("Este veículo já está cancelado.");
            }
        }

        public void Reactivate()
        {
            if (IsCancel)
            {
                IsCancel = false;
                UserCancelID = Guid.Empty;
            }
            else
            {
                throw new InvalidOperationException("Este veículo não está cancelado.");
            }
        }
    }
}