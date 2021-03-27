using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Gabos_Recruitment.Module.BusinessObjects
{
    [DomainComponent]
    [DefaultClassOptions]
    public abstract class BaseNonPersistentObject : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public BaseNonPersistentObject()
        {
            Oid = Guid.NewGuid();
        }
        [DevExpress.ExpressApp.Data.Key]
        [Browsable(false)]
        public Guid Oid { get; set; }

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
        }
        void IXafEntityObject.OnLoaded()
        {
            // Place the code that is executed each time the entity is loaded here.
        }
        void IXafEntityObject.OnSaving()
        {
            // Place the code that is executed each time the entity is saved here.
        }
        #endregion

        #region IObjectSpaceLink members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIObjectSpaceLinktopic.aspx)
        // Use the Object Space to access other entities from IXafEntityObject methods (see https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113707.aspx).
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
