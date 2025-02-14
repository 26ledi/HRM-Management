﻿using Application.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using Domain.Entities;
using HRManagement.Exceptions.Shared;
using Persistence.Repositories.Interfaces;

namespace Application.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> CreateAsync(UserRequest userRequest)
        {
            var userLooked = await _userRepository.GetByEmailAsync(userRequest.Email);

            if (userLooked is not null)
                throw new AlreadyExistsException($"A user with this email: {userRequest.Email} already exists");

            var mappedUser = new User
            {
                Id = userRequest.Id,
                Email = userRequest.Email,
                Username = userRequest.Username    
            };

            await _userRepository.AddAsync(mappedUser);

            return new UserResponse()
            {
                Id = mappedUser.Id,
                Email = mappedUser.Email,
                Username = mappedUser.Username
            };
        }

        public async Task DeleteAsync(string email)
        {
            var userLooked = await _userRepository.GetByEmailAsync(email)
                                   ?? throw new NotFoundException($"The user with this email : {email} does not exist");

            await _userRepository.DeleteAsync(userLooked);
        }

        public async Task<List<UserResponse>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            if (users is null)
                throw new NotFoundException("There is no any user");

            return users.Select(user => new UserResponse()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username
            }).ToList();
        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var userLooked = await _userRepository.GetByIdAsync(userId)
                                       ?? throw new NotFoundException($"A user with this id : {userId} does not exist");
            return userLooked;
        }

        public async Task<UserResponse> UpdateAsync(string email, UserRequest userRequest)
        {
            var userLooked = await _userRepository.GetByEmailAsync(email);

            userLooked.Email = userRequest.Email;
            userLooked.Username = userRequest.Username;

            var updatedTask = await _userRepository.UpdateAsync(userLooked);

            return new UserResponse()
            {
                Id = updatedTask.Id,
                Email = updatedTask.Email,
                Username = updatedTask.Username
            };
        }
    }
}
