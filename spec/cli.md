# Command Line Interface Specification

This document serves to specify all commands and options that are invokable 
from the Skaf command line interface (CLI). Skaf follows the common 
`skaf <VERB> <ARGS>` pattern. Usage examples for each verb are included in this 
document.

## init

The `init` command is used to construct a new configuration file that is used 
by `skaf`. The command prompts the user with a set of questions that are used 
write the configuration file that will best suit there needs.

| Option          | Short Name | Description                                                              |
| --------------- | ---------- | ------------------------------------------------------------------------ |
| --quiet         | -q         | Uses the default answers to the questionnaire when generating the config |
| --output <PATH> | -o <PATH>  | The path to which to place the new skaf configuration file               |

### Examples

The following demonstrate some common usages of the `skaf init` command.

#### Simple Initialization

```bash
skaf init;
```

This command will launch the questionnaire which in turn will generate a new 
`skaf.json` configuration in the current directory.

#### Quiet/Quick Initialization

```bash
# Long Form
skaf init --quiet;

# Short Form
skaf init -q;
```

Running either of these commands will build a `skaf.json` configuration file 
with default values for the configuration. This command is great for getting 
started with `skaf` as quickly as possible.

#### Custom Configuration Name

```bash
# Long Form
skaf init --output ./custom_name.json --quiet;

# Short Form
skaf init -o ./custom_name.json -q;
```

Running either of these commands will build a `custom_name.json` configuration 
file in the current directory with default values for the configuration options.
Typically this is not recommended but can be used if multiple configuration 
files need to be used within the same directory.

## update

The `update` commmand is used to build tests as defined by a skaf configuration 
file.

| Option          | Short Name | Description                                                                             |
| --------------- | ---------- | --------------------------------------------------------------------------------------- |
| --config <PATH> | -c <PATH>  | Uses the specified configuration file when updating the tests.                          |
| --watch         | -w         | Runs the update command and automatically reruns it when any of the input files change. |

### Examples

The following demonstrate some common usages of the `skaf update` command.

#### Single Update

```bash
skaf update;
```

This is the simplest form of the `skaf update` command. This will update the 
tests based on the `skaf.json` configuration file in the current directory.

#### Watch Update

```bash
# Long Form
skaf update --watch;

# Short Form
skaf update -w;
```

Either of these commands will automatically rerun `skaf update` using the 
`skaf.json` configuration when either the set of input files or any files 
within the set have changed. This allows the developer to run the command once 
and develop indefinitely without having to worry about rerunning `skaf update`.