using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Abstractions;
using WebApplication1.Data.Repository;
using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public class ModeratorService : IModeratorService
    {
        private readonly ITestRepository _testRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IAccessRepository _accessRepository;
        private readonly ITestResultRepository _testResultRepository;
        public ModeratorService(ITestRepository testRepository,
                                IMapper mapper,
                                IUserRepository userRepository,
                                IAccessRepository accessRepository,
                                ITestResultRepository testResultRepository)
        {
            _testRepository = testRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _accessRepository = accessRepository;
            _testResultRepository = testResultRepository;
        }

        public async Task<List<TestResponse>> GetTests(Guid Id)
        {
            var testEntity = await _testRepository.GetByCompanyNameAndPublic(Id);

            var response = testEntity
                .Select(x => new TestResponse(
                    x.Id,
                    x.Title,
                    x.Description,
                    x.Category,
                    x.Level,
                    x.CreatedDate.UtcDateTime,
                    x.IsPrivate,
                    x.CompanyOwners)).ToList();

            return response;
        }

        public async Task<TestFullInfo> GetFullTestById(Guid userId, Guid testId)
        {
            var HasModeratorAccesToTest = await _testRepository.HasModeratorAccesToTest(userId, testId);

            if (!HasModeratorAccesToTest)
                throw new Exception("Test not found or you Havn't access");

            return await _testRepository.GetFullTestInfo(testId);
        }

        public async Task<List<UserResponse>> GetUsers(Guid asckerId)
        {
            var userList = await _userRepository.GetAll(asckerId);

            return userList.Select(x => _mapper.Map<UserResponse>(x)).ToList();
        }

        public async Task<List<UserResponse>> GetUsersHavntAccessToTest(Guid asckerId, Guid testId)
        {
            var userList = await _userRepository.GetUsersHavntAccessToTest(asckerId, testId);

            return userList.Select(x => _mapper.Map<UserResponse>(x)).ToList();
        }


        public async Task PostAccessUsersToTest(Guid asckerId, Guid testId, List<AccessRequest> accessList)
            => await _accessRepository.GetAccessToUsers(asckerId, testId, accessList);

        public async Task<TestStatisticResponse> GetTestStatistic(
            Guid asckerId,
            Guid testId,
            bool isPrivate,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            return await _testRepository.GetTestStatistic(asckerId, testId, isPrivate, startDate, endDate);
        }


        public async Task<TestResultResponse> GetTestResultStaffUser(Guid askerId, Guid userId, Guid testResultId)
        {

            var testResult = _mapper
            .Map<TestResultResponse>(await
                _testResultRepository.GetById(testResultId));

            var userQuestionsResult = await _testResultRepository.GetUserOptionsResultResponsesByTestResultId(askerId, testResultId, userId);

            testResult.UserQuestionResult.AddRange(userQuestionsResult.ToList());

            return testResult;
        }


        public async Task<List<TestResultResponse>> GetUserResults(Guid id, Guid staffId, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var testResultList = await _testResultRepository.GetUserResults(staffId, id, startDate, endDate);

            return testResultList.Select(x => _mapper.Map<TestResultResponse>(x)).ToList();
        }

        public async Task<bool> DeleteUserTestResult(Guid id, Guid staffId, Guid resultId)
            => await _testResultRepository.DeleteUserTestResult(id, staffId, resultId);


    }
}
