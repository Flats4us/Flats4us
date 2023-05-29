﻿namespace Flats4us.Helpers.Enums
{
    public enum Gender
    {
        Male,
        Female,
        Both
    }

    public enum OfferStatus
    {
        Current,
        Outdated,
        Suspended,
        Rented
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
        Unfounded,
        ResolvedByMod,
        UnfoundedByMod
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
        NotVerified
    }

    public enum PaymentPurpose
    {
        Rent,
        Deposit,
        Repairs
    }

    public enum PromotionType
    {
        type1,
        type2
    }

    public enum RoommatesStatus
    {
        Roommate,
        Alone
    }
}
