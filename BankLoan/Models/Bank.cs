using BankLoan.Models.Contracts;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private List<ILoan> loans;
        private List<IClient> clients;

        public Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            this.loans = new List<ILoan>();
            this.clients = new List<IClient>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public int Capacity { get; private set; }

        public IReadOnlyCollection<ILoan> Loans => loans.AsReadOnly();

        public IReadOnlyCollection<IClient> Clients => clients.AsReadOnly();

        public void AddClient(IClient Client)
        {
            if (Clients.Count < Capacity)
            {
                this.clients.Add(Client);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.NotEnoughCapacity);
            }
        }

        public void AddLoan(ILoan loan)
        {
            this.loans.Add(loan);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Name: {Name}, Type: {GetType().Name}");
            sb.Append($"Clients: ");
            if (this.clients.Any())
            {
                var names = this.Clients.Select(p => p.Name);
                sb.AppendLine(string.Join(", ", names));
            }
            else
            {
                sb.AppendLine("none");
            }
            sb.AppendLine($"Loans: {this.loans.Count}, Sum of Rates: {SumRates()}");


            return sb.ToString().TrimEnd();
        }

        public void RemoveClient(IClient client)
            => this.clients.Remove(client);

        public double SumRates()
        {
            if (this.Loans.Count == 0)
            {
                return 0;
            }
            return this.loans.Sum(r => r.InterestRate);
        }

    }
}
