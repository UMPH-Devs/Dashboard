//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dashboard.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class RefModuleType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RefModuleType()
        {
            this.ModuleStatus = new HashSet<ModuleStatu>();
        }
    
        public int RefModuleTypeId { get; set; }
        public string AppId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public Nullable<int> Frequency { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public bool TokenRequired { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ModuleStatu> ModuleStatus { get; set; }
    }
}
