#!/usr/bin/env python2.7

import os
import subprocess

base_dir = os.path.dirname(os.path.realpath(__file__))
cli_dir = os.path.join(base_dir, 'Cli')


def main():
    os.chdir(base_dir)
    run_cli_from_cli()


def run_cli_from_cli():
    os.chdir(cli_dir)
    output = subprocess.check_output(['dotnet', 'run'])
    assert_equal('Cli/appsettings.json', output.strip())


def assert_equal(expected, actual):
    assert expected == actual, 'Incorrect output\nexpect: <{}>\nactual: <{}>'.format(expected, actual)


if __name__ == '__main__':
    main()
