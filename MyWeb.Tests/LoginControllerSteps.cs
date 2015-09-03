﻿using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyWeb.Controllers;
using TechTalk.SpecFlow;

namespace MyWeb.Tests.Steps
{
	[Binding]
	[Scope(Feature = "LoginController")]
	public class LoginControllerSteps
	{
		private LoginController _target;

		[BeforeScenario]
		public void BeforeScenario()
		{
			this._target = new LoginController();
		}

		[Given(@"login account is ""(.*)""")]
		public void GivenLoginAccountIs(string account)
		{
			ScenarioContext.Current.Set<string>(account, "account");
		}

		[Given(@"user's password is ""(.*)""")]
		public void GivenUserSPasswordIs(string password)
		{
			ScenarioContext.Current.Set<string>(password, "password");
		}

		[When(@"I invoke Index with HttpPost")]
		public void WhenIInvokeIndexWithHttpPost()
		{
			var account = ScenarioContext.Current.Get<string>("account");
			var password = ScenarioContext.Current.Get<string>("password");

			var actual = this._target.Index(account, password);

			ScenarioContext.Current.Set<ActionResult>(actual);
		}

		[Then(@"result's Controller name should be ""(.*)""")]
		public void ThenResultSControllerNameShouldBe(string controllerName)
		{
			var actual = ScenarioContext.Current.Get<ActionResult>() as RedirectToRouteResult;

			Assert.IsNotNull(actual);
			Assert.AreEqual(controllerName, actual.RouteValues["Controller"]);
		}

		[Then(@"result's Action name should be ""(.*)""")]
		public void ThenResultSActionNameShouldBe(string actionName)
		{
			var actual = ScenarioContext.Current.Get<ActionResult>() as RedirectToRouteResult;

			Assert.IsNotNull(actual);
			Assert.AreEqual(actionName, actual.RouteValues["Action"]);
		}
	}
}