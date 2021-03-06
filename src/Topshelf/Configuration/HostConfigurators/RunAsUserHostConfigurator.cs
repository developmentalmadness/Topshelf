﻿// Copyright 2007-2012 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Topshelf.HostConfigurators
{
    using System;
    using System.Collections.Generic;
    using System.ServiceProcess;
    using Builders;
    using Configurators;

    public class RunAsUserHostConfigurator :
        HostBuilderConfigurator
    {
        readonly string _password;
        readonly string _username;

        public RunAsUserHostConfigurator(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public HostBuilder Configure(HostBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            builder.Match<InstallBuilder>(x => x.RunAs(_username, _password, ServiceAccount.User));

            return builder;
        }

        public IEnumerable<ValidateResult> Validate()
        {
            if (string.IsNullOrEmpty(_username))
                yield return this.Failure("Username", "must be specified for a User account type");
            if (string.IsNullOrEmpty(_password))
                yield return this.Failure("Password", "must be specified for a User account type");
        }
    }
}