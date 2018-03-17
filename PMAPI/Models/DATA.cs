namespace PMAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DATA : DbContext
    {
        public DATA()
            : base("name=SERVER")
        {
        }

        public virtual DbSet<Contractor> Contractors { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<ParameterCategory> ParameterCategories { get; set; }
        public virtual DbSet<ParameterName> ParameterNames { get; set; }
        public virtual DbSet<Parameter> Parameters { get; set; }
        public virtual DbSet<ParametersUnitType> ParametersUnitTypes { get; set; }
        public virtual DbSet<ParemeterCalcGroup> ParemeterCalcGroups { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VoltageLevel> VoltageLevels { get; set; }
        public virtual DbSet<WorkOrder> WorkOrders { get; set; }
        public virtual DbSet<WorkOrdersParameter> WorkOrdersParameters { get; set; }
        public virtual DbSet<WorkOrderStatu> WorkOrderStatus { get; set; }
        public virtual DbSet<WorkorderType> WorkorderTypes { get; set; }
        public virtual DbSet<WorkOrdersPhoto> WorkOrdersPhotos { get; set; }
        public virtual DbSet<userChieldView> userChieldViews { get; set; }
        public virtual DbSet<WorkOrderParametersView> WorkOrderParametersViews { get; set; }
        public virtual DbSet<WorkOrderView> WorkOrderViews { get; set; }
        public virtual DbSet<V_Contractor_noOfWorkorders_workorderType> V_Contractor_noOfWorkorders_workorderType { get; set; }
        public virtual DbSet<V_Contractor_noOfAchievedWorkorders_workorderType> V_Contractor_noOfAchievedWorkorders_workorderType { get; set; }
        public virtual DbSet<V_Project_totalLengths_project_contractor> V_Project_totalLengths_project_contractor { get; set; }
        public virtual DbSet<V_Project_totalAmounts_project_contractor_worktype1> V_Project_totalAmounts_project_contractor_worktype1 { get; set; }
        public virtual DbSet<V_Project_percentOfAchievement_contractors> V_Project_percentOfAchievement_contractors { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Contractor>()
            //    .HasMany(e => e.WorkOrders)
            //    .WithOptional(e => e.Contractor)
            //    .HasForeignKey(e => e.contractors_FKID);

            //modelBuilder.Entity<Location>()
            //    .HasMany(e => e.WorkOrders)
            //    .WithOptional(e => e.Location)
            //    .HasForeignKey(e => e.location_FKID);

            //modelBuilder.Entity<ParameterCategory>()
            //    .HasMany(e => e.Parameters)
            //    .WithOptional(e => e.ParameterCategory)
            //    .HasForeignKey(e => e.parameterCategory_FKID);

            //modelBuilder.Entity<ParameterCategory>()
            //    .HasMany(e => e.WorkOrders)
            //    .WithOptional(e => e.ParameterCategory)
            //    .HasForeignKey(e => e.ParameterCategory_FKID);

            //modelBuilder.Entity<ParameterName>()
            //    .HasMany(e => e.Parameters)
            //    .WithOptional(e => e.ParameterName)
            //    .HasForeignKey(e => e.parameterNameID_FK)
            //    .WillCascadeOnDelete();

            //modelBuilder.Entity<Parameter>()
            //    .HasMany(e => e.WorkOrdersParameters)
            //    .WithOptional(e => e.Parameter)
            //    .HasForeignKey(e => e.parameter_FKID);

            //modelBuilder.Entity<ParametersUnitType>()
            //    .HasMany(e => e.Parameters)
            //    .WithOptional(e => e.ParametersUnitType)
            //    .HasForeignKey(e => e.parameterUnitType_FKID);

            //modelBuilder.Entity<ParemeterCalcGroup>()
            //    .HasMany(e => e.Parameters)
            //    .WithOptional(e => e.ParemeterCalcGroup)
            //    .HasForeignKey(e => e.parameterCalcGroup_FKID);

            //modelBuilder.Entity<Project>()
            //    .HasMany(e => e.WorkOrders)
            //    .WithOptional(e => e.Project)
            //    .HasForeignKey(e => e.project_FKID);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.Users1)
            //    .WithOptional(e => e.User1)
            //    .HasForeignKey(e => e.SuperVisor);

            //modelBuilder.Entity<User>()
            //    .HasMany(e => e.WorkOrders)
            //    .WithOptional(e => e.User)
            //    .HasForeignKey(e => e.user_FKID);

            //modelBuilder.Entity<VoltageLevel>()
            //    .HasMany(e => e.WorkOrders)
            //    .WithOptional(e => e.VoltageLevel)
            //    .HasForeignKey(e => e.voltageLevels_FKID);

            //modelBuilder.Entity<WorkOrder>()
            //    .HasMany(e => e.WorkOrdersParameters)
            //    .WithOptional(e => e.WorkOrder)
            //    .HasForeignKey(e => e.workOrder_FKID);

            //modelBuilder.Entity<WorkOrderStatu>()
            //    .HasMany(e => e.WorkOrders)
            //    .WithOptional(e => e.WorkOrderStatu)
            //    .HasForeignKey(e => e.workOrderStatus_FKID);

            //modelBuilder.Entity<WorkorderType>()
            //    .HasMany(e => e.ParameterCategories)
            //    .WithOptional(e => e.WorkorderType)
            //    .HasForeignKey(e => e.workorderTypeID_FK);
        }
        
    }
}
