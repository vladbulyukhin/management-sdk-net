﻿using FluentAssertions;
using Kontent.Ai.Management.Models.Shared;
using Kontent.Ai.Management.Models.Subscription;
using Kontent.Ai.Management.Tests.Base;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Kontent.Ai.Management.Tests.ManagementClientTests;

public class SubscriptionTests : IClassFixture<FileSystemFixture>
{
    private readonly FileSystemFixture _fileSystemFixture;
    private readonly string _subscriptionEndpoint;

    public SubscriptionTests(FileSystemFixture fileSystemFixture)
    {
        _fileSystemFixture = fileSystemFixture;
        _fileSystemFixture.SetSubFolder("Subscription");
        _subscriptionEndpoint = $"{FileSystemFixture.Endpoint}/subscriptions/{FileSystemFixture.SUBCRIPTION_ID}";
    }

    [Fact]
    public async Task ListSubscriptionProjects_ListsSubscriptionProjects()
    {
        var expected = _fileSystemFixture.GetItemsOfExpectedListingResponse<SubscriptionProjectModel>("Projects.json");
        var client = _fileSystemFixture.CreateMockClientWithResponse("Projects.json");

        var response = await client.ListSubscriptionProjectsAsync();

        response.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task ListSubscriptionUsers_ListsSubscriptionUsers()
    {
        var expected = _fileSystemFixture.GetItemsOfExpectedListingResponse<SubscriptionUserModel>("Users.json");
        var client = _fileSystemFixture.CreateMockClientWithResponse("Users.json");

        var response = await client.ListSubscriptionUsersAsync();

        response.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetSubscriptionUser_ById_GetsSubscriptionUser()
    {
        var expected = _fileSystemFixture.GetExpectedResponse<SubscriptionUserModel>("User.json");

        var identifier = UserIdentifier.ById(expected.Id);

        var client = _fileSystemFixture.CreateMockClientWithResponse("User.json");

        var response = await client.GetSubscriptionUserAsync(identifier);

        response.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetSubscriptionUser_ByEmail_GetsSubscriptionUser()
    {
        var expected = _fileSystemFixture.GetExpectedResponse<SubscriptionUserModel>("User.json");

        var identifier = UserIdentifier.ByEmail(expected.Email);

        var client = _fileSystemFixture.CreateMockClientWithResponse("User.json");

        var response = await client.GetSubscriptionUserAsync(identifier);

        response.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task GetSubscriptionUser_ByNull_ThrowsException()
    {
        var client = _fileSystemFixture.CreateMockClientWithoutResponse();

        await client.Invoking(x => x.GetSubscriptionUserAsync(null)).Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    [Fact]
    public async void ActivateUser_ById_ActivatesUser()
    {
        var expected = _fileSystemFixture.GetExpectedResponse<SubscriptionUserModel>("User.json");

        var identifier = UserIdentifier.ById(expected.Id);

        var client = _fileSystemFixture.CreateMockClientWithUrl(expectedUrl: $"{_subscriptionEndpoint}/users/{expected.Id}/activate");

        Func<Task> activateUser = async () => await client.ActivateSubscriptionUserAsync(identifier);

        await activateUser.Should().NotThrowAsync();
    }

    [Fact]
    public async void ActivateUser_ByEmail_ActivatesUser()
    {
        var expected = _fileSystemFixture.GetExpectedResponse<SubscriptionUserModel>("User.json");

        var identifier = UserIdentifier.ByEmail(expected.Email);

        var client = _fileSystemFixture.CreateMockClientWithUrl(expectedUrl: $"{_subscriptionEndpoint}/users/email/{expected.Email}/activate");

        Func<Task> activateUser = async () => await client.ActivateSubscriptionUserAsync(identifier);

        await activateUser.Should().NotThrowAsync();
    }

    [Fact]
    public async Task ActivateUser_ByNull_ThrowsException()
    {
        var client = _fileSystemFixture.CreateMockClientWithoutResponse();

        await client.Invoking(x => x.ActivateSubscriptionUserAsync(null)).Should().ThrowExactlyAsync<ArgumentNullException>();
    }

    [Fact]
    public async void DeactivateUser_ById_DeactivatesUser()
    {
        var expected = _fileSystemFixture.GetExpectedResponse<SubscriptionUserModel>("User.json");

        var identifier = UserIdentifier.ById(expected.Id);

        var client = _fileSystemFixture.CreateMockClientWithUrl(expectedUrl: $"{_subscriptionEndpoint}/users/{expected.Id}/deactivate");

        Func<Task> activateUser = async () => await client.DeactivateSubscriptionUserAsync(identifier);

        await activateUser.Should().NotThrowAsync();
    }

    [Fact]
    public async void DeactivateUser_ByEmail_DectivatesUser()
    {
        var expected = _fileSystemFixture.GetExpectedResponse<SubscriptionUserModel>("User.json");

        var identifier = UserIdentifier.ByEmail(expected.Email);

        var client = _fileSystemFixture.CreateMockClientWithUrl(expectedUrl: $"{_subscriptionEndpoint}/users/email/{expected.Email}/deactivate");

        Func<Task> activateUser = async () => await client.DeactivateSubscriptionUserAsync(identifier);

        await activateUser.Should().NotThrowAsync();
    }

    [Fact]
    public async Task DeactivateUser_ByNull_ThrowsException()
    {
        var client = _fileSystemFixture.CreateMockClientWithoutResponse();

        await client.Invoking(x => x.DeactivateSubscriptionUserAsync(null)).Should().ThrowExactlyAsync<ArgumentNullException>();
    }
}
