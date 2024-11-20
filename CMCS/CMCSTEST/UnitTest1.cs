using CMCS.Controllers;
using CMCS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class UnitTest1
{
    private readonly AccountController _accountController;
    private readonly ClaimController _claimController;

    public UnitTest1()
    {
        _accountController = new AccountController();
        _claimController = new ClaimController();
    }

    [Fact]
    public void LoginRegister_GET_ReturnsViewResult()
    {
        var result = _accountController.LoginRegister(null);
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.NotNull(viewResult);
    }


    [Fact]
    public async Task LoginRegister_POST_Login_InvalidUser_ReturnsViewResult()
    {
        var invalidUser = new User { Email = "wrong@example.com", Password = "WrongPassword", Name = "Test", Surname = "User", Role = "Lecturer" };
        var result = await _accountController.LoginRegister(invalidUser, "Login");
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.NotNull(viewResult);
        Assert.False(_accountController.ModelState.IsValid);
    }


    [Fact]
    public void Logout_RedirectsToHome()
    {
        var result = _accountController.Logout();
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectResult.ActionName);
        Assert.Equal("Home", redirectResult.ControllerName);
    }

    [Fact]
    public void SubmitClaim_GET_ReturnsViewResult()
    {
        var result = _claimController.SubmitClaim();
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.NotNull(viewResult);
    }


    [Fact]
    public async Task SubmitClaim_POST_InvalidClaim_ReturnsViewWithModelErrors()
    {
        var claim = new CMCS.Models.Claim();  
        _claimController.ModelState.AddModelError("SupportingDocument", "You must upload a file.");

        var result = await _claimController.SubmitClaim(claim);
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.False(viewResult.ViewData.ContainsKey("IsSuccess"));
        Assert.False(_claimController.ModelState.IsValid);
    }

}
