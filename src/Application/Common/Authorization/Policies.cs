namespace VetCheckup.Application.Common.Authorization;

public static class Policies
{
    public const string CanManageVets = "CanManageVets";
    public const string CanManageOwners = "CanManageOwners";
    public const string CanManagePets = "CanManagePets";
    public const string CanManageOrganisations = "CanManageOrganisations";
    public const string CanViewMedicalRecords = "CanViewMedicalRecords";
}
