//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lm.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class td_Project
    {
        public int Id { get; set; }
        public string Project_Name { get; set; }
        public string Project_Location { get; set; }
        public int Project_People { get; set; }
        public System.DateTime Project_Start { get; set; }
        public System.DateTime Project_End { get; set; }
        public string Project_Description { get; set; }
        public System.DateTime Create_date { get; set; }
        public Nullable<int> Project_ImgId { get; set; }
        public Nullable<int> Create_People { get; set; }
    }
}
