using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomicObjectDesigner.Models.Registration;

namespace AutomicObjectDesignerBack.Data.Configurations;

// Here you can add user on model creation - for example an Admin account.
public class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserModel> builder)
    {
        builder.HasData(
            new UserModel
            {
                Id = 100,
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "Admin",
                Email = "admin@gmail.com",
                HasEmailConfirmed = true,
                Role = "Admin"
            }
        );
    }
}