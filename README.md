# Stanford-password-policy-dotnet [![Codacy Badge](https://api.codacy.com/project/badge/Grade/ceff2f1be7474d31ad7e87cc5c1c8ff8)](https://www.codacy.com/app/dim5/stanford-password-policy-dotnet?utm_source=github.com&utm_medium=referral&utm_content=dim5/stanford-password-policy-dotnet&utm_campaign=Badge_Grade) [![Build status](https://ci.appveyor.com/api/projects/status/3utww07uaw0l2uq2/branch/master?svg=true)](https://ci.appveyor.com/project/dim5/stanford-password-policy-dotnet/branch/master)

Stanford-password-policy-dotnet is a password validator library for ASP.NET Core.

## What is it

The [Stanford password policy](https://uit.stanford.edu/service/accounts/passwords/quickguide) is a dynamic password policy that
encourages the use of easy to remember, yet secure passphrases instead of hard to remember passwords.

## Installation

Using the dotnet-cli

```bash
dotnet add package StanfordPasswordPolicy
```

or with the nuget package manager console:

```bash
Install-Package StanfordPasswordPolicy
```

## Usage

```csharp
services.AddIdentity<AppUser, IdentityRole>(opt =>
        {
            // If you don't want Identity's defaults to interfere with your new policy
            opt.Password = StanfordPasswordValidatorBase.NoDefaultPasswordOptions;
        })
        .AddPasswordValidator<StanfordPasswordValidator<AppUser>>();
```

You can also check out SampleApp for a more complete example.

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[Apache License 2.0](https://choosealicense.com/licenses/apache-2.0/)

## Obligatory XKCD

[![XKCD#936](https://imgs.xkcd.com/comics/password_strength.png)](https://xkcd.com/936)
