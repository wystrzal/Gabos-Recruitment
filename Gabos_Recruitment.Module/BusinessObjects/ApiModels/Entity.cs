using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using Gabos_Recruitment.Module.BusinessObjects.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Gabos_Recruitment.Module.BusinessObjects.ApiModels
{
    [DomainComponent]
    [DefaultClassOptions]
    public class Entity : BaseNonPersistentObject, IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        public Entity()
        {
            Oid = Guid.NewGuid();
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string nip;
        public string Nip
        {
            get { return nip; }
            set
            {
                nip = value;
                OnPropertyChanged();
            }
        }

        private string statusVat;
        public string StatusVat
        {
            get { return statusVat; }
            set
            {
                statusVat = value;
                OnPropertyChanged();
            }
        }

        private string regon;
        public string Regon
        {
            get { return regon; }
            set
            {
                regon = value;
                OnPropertyChanged();
            }
        }

        private string pesel;
        public string Pesel
        {
            get { return pesel; }
            set
            {
                pesel = value;
                OnPropertyChanged();
            }
        }

        private string krs;
        public string Krs
        {
            get { return krs; }
            set
            {
                krs = value;
                OnPropertyChanged();
            }
        }

        private string residenceAddress;
        public string ResidenceAddress
        {
            get { return residenceAddress; }
            set
            {
                residenceAddress = value;
                OnPropertyChanged();
            }
        }

        private string workingAddress;
        public string WorkingAddress
        {
            get { return workingAddress; }
            set
            {
                workingAddress = value;
                OnPropertyChanged();
            }
        }

        private List<EntityPerson> representatives;
        public List<EntityPerson> Representatives
        {
            get { return representatives; }
            set
            {
                representatives = value;
                OnPropertyChanged();
            }
        }

        private List<EntityPerson> authorizedClerks;
        public List<EntityPerson> AuthorizedClerks
        {
            get { return authorizedClerks; }
            set
            {
                authorizedClerks = value;
                OnPropertyChanged();
            }
        }

        private List<EntityPerson> partners;
        public List<EntityPerson> Partners
        {
            get { return partners; }
            set
            {
                partners = value;
                OnPropertyChanged();
            }
        }

        private DateTime? registrationLegalDate;
        public DateTime? RegistrationLegalDate
        {
            get { return registrationLegalDate; }
            set
            {
                registrationLegalDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime? registrationDenialDate;
        public DateTime? RegistrationDenialDate
        {
            get { return registrationDenialDate; }
            set
            {
                registrationDenialDate = value;
                OnPropertyChanged();
            }
        }

        private string registrationDenialBasis;
        public string RegistrationDenialBasis
        {
            get { return registrationDenialBasis; }
            set
            {
                registrationDenialBasis = value;
                OnPropertyChanged();
            }
        }

        private DateTime? restorationDate;
        public DateTime? RestorationDate
        {
            get { return restorationDate; }
            set
            {
                restorationDate = value;
                OnPropertyChanged();
            }
        }

        private string restorationBasis;
        public string RestorationBasis
        {
            get { return restorationBasis; }
            set
            {
                restorationBasis = value;
                OnPropertyChanged();
            }
        }

        private DateTime? removalDate;
        public DateTime? RemovalDate
        {
            get { return removalDate; }
            set
            {
                removalDate = value;
                OnPropertyChanged();
            }
        }

        private string removalBasis;
        public string RemovalBasis
        {
            get { return removalBasis; }
            set
            {
                removalBasis = value;
                OnPropertyChanged();
            }
        }

        private List<string> accountNumbers;
        public List<string> AccountNumbers
        {
            get { return accountNumbers; }
            set
            {
                accountNumbers = value;
                OnPropertyChanged();
            }
        }

        private List<AccountNumber> bankAccountNumbers = new List<AccountNumber>();
        public List<AccountNumber> BankAccountNumbers
        {
            get { return bankAccountNumbers; }
            set
            {
                bankAccountNumbers = value;
                OnPropertyChanged();
            }
        }

        private bool hasVirtualAccounts;
        public bool HasVirtualAccounts
        {
            get { return hasVirtualAccounts; }
            set
            {
                hasVirtualAccounts = value;
                OnPropertyChanged();
            }
        }


        #region IObjectSpaceLink members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIObjectSpaceLinktopic.aspx)
        // Use the Object Space to access other entities from IXafEntityObject methods (see https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113707.aspx).
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion
    }
}
