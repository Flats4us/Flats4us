namespace Flats4us.Helpers.Enums
{
    public enum TechnicalProblemType
    {
        ApplicationOperation,
        Payment,
        AccountPerformance,
        Other
    }
    public enum Gender
    {
        Male,
        Female,
        Both
    }

    public enum OfferStatus
    {
        Current,
        Waiting,
        Rented,
        Old
    }

    public enum Sender
    {
        Student,
        Owner
    }

    public enum ArgumentStatus
    {
        Ongoing,
        Resolved,
        Unfounded
    }

    public enum DocumentType
    {
        ID,
        StudentCard,
        Passport
    }

    public enum VerificationStatus
    {
        Verified,
        NotVerified,
        Rejected
    }

    public enum PaymentPurpose
    {
        Rent,
        Deposit,
        Repairs
    }

    public enum RoommatesStatus
    {
        Roommate,
        Alone
    }

    public enum PropertyType
    {
        Flat,
        House,
        Room
    }

    public enum UserType
    {
        Owner,
        Student,
        Moderator
    }
}
