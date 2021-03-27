using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Gabos_Recruitment.Module.BusinessObjects.Specification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Gabos_Recruitment.Module.BusinessObjects
{
    [DomainComponent]
    [DefaultClassOptions]
    public class SearchSubject : BaseNonPersistentObject, IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        public SearchSubject()
        {
            Oid = Guid.NewGuid();
        }

        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        private List<BankAccount> bankAccounts;
        public List<BankAccount> BankAccounts
        {
            get { return bankAccounts; }
            set
            {
                bankAccounts = value;
                OnPropertyChanged();
            }
        }

        private List<Nip> nips;
        public List<Nip> Nips
        {
            get { return nips; }
            set
            {
                nips = value;
                OnPropertyChanged();
            }
        }

        private List<Regon> regons;
        public List<Regon> Regons
        {
            get { return regons; }
            set
            {
                regons = value;
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