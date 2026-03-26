CREATE TABLE companies (
    Id TEXT NOT NULL PRIMARY KEY,
    Name TEXT NOT NULL
);

CREATE TABLE positions (
    Id TEXT NOT NULL PRIMARY KEY,
    Name TEXT NOT NULL
);

CREATE TABLE employees (
    Id TEXT NOT NULL PRIMARY KEY,
    Name TEXT NOT NULL,
    Surname TEXT NOT NULL,
    Email TEXT NOT NULL,
    company_id TEXT NOT NULL,
    FOREIGN KEY (company_id) REFERENCES companies(Id) ON DELETE CASCADE
);

CREATE TABLE projects (
    Id TEXT NOT NULL PRIMARY KEY,
    Name TEXT NOT NULL,
    company_customer_id TEXT NOT NULL,
    company_executor_id TEXT NOT NULL,
    FOREIGN KEY (company_customer_id) REFERENCES companies(Id) ON DELETE CASCADE,
    FOREIGN KEY (company_executor_id) REFERENCES companies(Id) ON DELETE CASCADE
);

CREATE TABLE employee_positions (
    EmployeesId TEXT NOT NULL,
    PositionsId TEXT NOT NULL,
    PRIMARY KEY (EmployeesId, PositionsId),
    FOREIGN KEY (EmployeesId) REFERENCES employees(Id) ON DELETE CASCADE,
    FOREIGN KEY (PositionsId) REFERENCES positions(Id) ON DELETE CASCADE
);

CREATE TABLE project_employees (
    EmployeesId TEXT NOT NULL,
    ProjectsId TEXT NOT NULL,
    PRIMARY KEY (EmployeesId, ProjectsId),
    FOREIGN KEY (EmployeesId) REFERENCES employees(Id) ON DELETE CASCADE,
    FOREIGN KEY (ProjectsId) REFERENCES projects(Id) ON DELETE CASCADE
);

CREATE INDEX IX_employees_company_id ON employees(company_id);
CREATE INDEX IX_employee_positions_PositionsId ON employee_positions(PositionsId);
CREATE INDEX IX_project_employees_ProjectsId ON project_employees(ProjectsId);
CREATE INDEX IX_projects_company_customer_id ON projects(company_customer_id);
CREATE INDEX IX_projects_company_executor_id ON projects(company_executor_id);
