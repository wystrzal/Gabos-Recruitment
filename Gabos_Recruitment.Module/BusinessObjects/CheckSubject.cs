using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
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
    public class CheckSubject : BaseNonPersistentObject, IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        public CheckSubject()
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

        private List<BankAccount> bankAccount;
        public List<BankAccount> BankAccount
        {
            get { return bankAccount; }
            set
            {
                bankAccount = value;
                OnPropertyChanged();
            }
        }

        private List<Nip> nip;
        public List<Nip> Nip
        {
            get { return nip; }
            set
            {
                nip = value;
                OnPropertyChanged();
            }
        }

        private List<Regon> regon;
        public List<Regon> Regon
        {
            get { return regon; }
            set
            {
                regon = value;
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