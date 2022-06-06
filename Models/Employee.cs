namespace BookStoreP4.Models {
    public class Employee {
        public int EmployeeID { get; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeeEmail { get; }
        public string EmployeeStreet { get; }
        public string EmployeeCity { get; }
        public string? EmployeePESEL { get; }

        public Employee(int employeeID, string employeeName, string employeeSurname, string employeeEmail, string employeeStreet, string employeeCity, string? employeePESEL = null) {
            EmployeeID = employeeID;
            EmployeeName = employeeName;
            EmployeeSurname = employeeSurname;
            EmployeeEmail = employeeEmail;
            EmployeeStreet = employeeStreet;
            EmployeeCity = employeeCity;
            EmployeePESEL = employeePESEL;
        }
        public Employee(int employeeID, string employeeName, string employeeSurname, string employeeEmail, string employeeStreet, string employeeCity) {
            EmployeeID = employeeID;
            EmployeeName = employeeName;
            EmployeeSurname = employeeSurname;
            EmployeeEmail = employeeEmail;
            EmployeeStreet = employeeStreet;
            EmployeeCity = employeeCity;
            EmployeePESEL = null;
        }
        public Employee(string employeeName, string employeeSurname, string employeeEmail, string employeeStreet, string employeeCity) {
            EmployeeName = employeeName;
            EmployeeSurname = employeeSurname;
            EmployeeEmail = employeeEmail;
            EmployeeStreet = employeeStreet;
            EmployeeCity = employeeCity;
            EmployeePESEL = null;
        }

        public override string ToString() {
            return $"{EmployeeName} {EmployeeSurname}";
        }
    }
}
