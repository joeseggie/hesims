namespace HESIMS.Web.Data.EntityTypeConfiguration;

public class StudentContactConfiguration : IEntityTypeConfiguration<StudentContact>
{
    public void Configure(EntityTypeBuilder<StudentContact> builder)
    {
        builder.HasKey(studentContact => studentContact.Id);

        builder.Property(studentContact => studentContact.ContactType).IsRequired();
        builder.Property(studentContact => studentContact.ContactValue).IsRequired();

        builder.HasOne(studentContact => studentContact.Student)
            .WithMany(student => student.StudentContacts)
            .HasForeignKey(studentContact => studentContact.StudentId)
            .IsRequired();
    }
}