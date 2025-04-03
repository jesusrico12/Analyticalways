using System;

namespace ACME.SchoolManagement.Core.Domain.Interfaces
{
    /// <summary>
    /// Interface for processing payments related to students.
    /// </summary>
    public interface IPaymentGateway
    {
        /// <summary>
        /// Processes a payment for a specific student.
        /// </summary>
        /// <param name="studentId">The unique identifier of the student.</param>
        /// <param name="amount">The payment amount.</param>
        /// <returns>
        /// True if the payment is successfully processed; otherwise, false.
        /// </returns>
        bool ProcessPayment(Guid studentId, decimal amount);
    }
}
