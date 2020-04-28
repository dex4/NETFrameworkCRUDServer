using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace MobileServer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Entry> entries { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.ValidateOnSaveEnabled = true;
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class Entry
    {
        public Entry() { }

        public Entry(string id, int bloodSugarLevel, int carbohydratesIntake, float insulinIntake, string entryTime, string entryHour, int physicalActivityDuration, string entryMomentSpecifier, string mealTypeSpecifier, bool hasDonePhysicalActivity)
        {
            this.id = id;
            this.bloodSugarLevel = bloodSugarLevel;
            this.carbohydratesIntake = carbohydratesIntake;
            this.insulinIntake = insulinIntake;
            this.entryTime = entryTime;
            this.entryHour = entryHour;
            this.physicalActivityDuration = physicalActivityDuration;
            this.entryMomentSpecifier = entryMomentSpecifier;
            this.mealTypeSpecifier = mealTypeSpecifier;
            this.hasDonePhysicalActivity = hasDonePhysicalActivity;
        }
        public string id { get; set; }
        public int bloodSugarLevel { get; set; }
        public int carbohydratesIntake { get; set; }
        public float insulinIntake { get; set; }
        public string entryTime { get; set; }
        public string entryHour { get; set; }
        public int physicalActivityDuration { get; set; }
        public string entryMomentSpecifier { get; set; }
        public string mealTypeSpecifier { get; set; }
        public bool hasDonePhysicalActivity { get; set; }
    }

    public class EntriesListDTO
    {
        public IEnumerable<Entry> entriesList;
    }
}