using FluentAssertions;
using Moq;
using ProjectManagment.Data.Entity;
using ProjectManagment.DTO;
using ProjectManagment.Interfaces;
using ProjectManagment.Services;

namespace ProjectManagment.Tests
{
    [TestFixture]
    public class ProjectServiceTests
    {
        private Mock<IRepository<Project>> _projectRepositoryMock;
        private Mock<IRepository<Company>> _companyRepositoryMock;
        private ProjectService _projectService;

        [SetUp]
        public void Setup()
        {
            _projectRepositoryMock = new Mock<IRepository<Project>>();
            _companyRepositoryMock = new Mock<IRepository<Company>>();
            _projectService = new ProjectService(_projectRepositoryMock.Object, _companyRepositoryMock.Object);
        }


        [Test]
        public async Task CreateProject_CorrectCreation_ShouldPass()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var executerId = Guid.NewGuid();
            var request = new ProjectCreateRequest
            {
                Name = "Test Project",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30)),
                Priority = 1,
                CustomerCompanyId = customerId,
                ExecuterCompanyId = executerId
            };

            var customerCompany = new Company { Id = customerId, Name = "Customer" };
            var executerCompany = new Company { Id = executerId, Name = "Executer" };
            var createdProject = (Project)request;

            _companyRepositoryMock.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customerCompany);
            _companyRepositoryMock.Setup(r => r.GetByIdAsync(executerId)).ReturnsAsync(executerCompany);
            _projectRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Project>())).ReturnsAsync(createdProject);

            // Act
            var result = await _projectService.CreateProjectAsync(request);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().Be(createdProject.Id);

            _companyRepositoryMock.Verify(r => r.GetByIdAsync(customerId), Times.Once);
            _companyRepositoryMock.Verify(r => r.GetByIdAsync(executerId), Times.Once);
            _projectRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Project>()), Times.Once);
        }

        [Test]
        public async Task CreateProject_CustomerNotExist_ShoudFail()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var executerId = Guid.NewGuid();

            var request = new ProjectCreateRequest
            {
                Name = "Test Project",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30)),
                Priority = 1,
                CustomerCompanyId = customerId,
                ExecuterCompanyId = executerId
            };

            var executerCompany = new Company { Id = executerId, Name = "Executer" };
            var createdProject = (Project)request;

            _companyRepositoryMock.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync((Company?)null);
            _companyRepositoryMock.Setup(r => r.GetByIdAsync(executerId)).ReturnsAsync(executerCompany);

            // Act
            var result = await _projectService.CreateProjectAsync(request);

            // Assert
            result.Success.Should().BeFalse();
        }

        [Test]
        public async Task CreateProject_ExecuterNotExist_ShoudFail()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var executerId = Guid.NewGuid();

            var request = new ProjectCreateRequest
            {
                Name = "Test Project",
                StartDate = DateOnly.FromDateTime(DateTime.Today),
                EndDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30)),
                Priority = 1,
                CustomerCompanyId = customerId,
                ExecuterCompanyId = executerId
            };

            var customerCompany = new Company { Id = customerId, Name = "Customer" };
            var createdProject = (Project)request;

            _companyRepositoryMock.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customerCompany);
            _companyRepositoryMock.Setup(r => r.GetByIdAsync(executerId)).ReturnsAsync((Company?)null);

            // Act
            var result = await _projectService.CreateProjectAsync(request);

            // Assert
            result.Success.Should().BeFalse();
        }

        [Test]
        public async Task CreateProject_StartDateGreaterThanEndDate_ShoudFail()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var executerId = Guid.NewGuid();

            var request = new ProjectCreateRequest
            {
                Name = "Test Project",
                StartDate = DateOnly.FromDateTime(DateTime.Today.AddDays(30)),
                EndDate = DateOnly.FromDateTime(DateTime.Today),
                Priority = 1,
                CustomerCompanyId = customerId,
                ExecuterCompanyId = executerId
            };

            var customerCompany = new Company { Id = customerId, Name = "Customer" };
            var executerCompany = new Company { Id = executerId, Name = "Executer" };

            _companyRepositoryMock.Setup(r => r.GetByIdAsync(customerId)).ReturnsAsync(customerCompany);
            _companyRepositoryMock.Setup(r => r.GetByIdAsync(executerId)).ReturnsAsync(executerCompany);

            // Act
            var result = await _projectService.CreateProjectAsync(request);

            // Assert
            result.Success.Should().BeFalse();
        }
    }
}
