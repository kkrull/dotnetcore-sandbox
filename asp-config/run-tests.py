#!/usr/bin/env python2.7

import os
import subprocess

base_dir = os.path.dirname(os.path.realpath(__file__))
cli_dir = os.path.join(base_dir, 'Cli')
cli_test_dir = os.path.join(base_dir, 'Cli.Test')
web_test_dir = os.path.join(base_dir, 'Web.Test')


def main():
    build_all()
    run_cli_in_own_path()
    run_cli_from_base_path()
    run_cli_test_in_own_path()
    run_cli_test_from_base_path()
    run_web_test_in_own_path()
    run_web_test_from_base_path()


def build_all():
    with cd(base_dir):
        subprocess.check_call(['dotnet', 'build'])


def run_cli_in_own_path():
    with cd(cli_dir):
        output = subprocess.check_output(['dotnet', 'run'])
        assert_equal('Cli/appsettings.json', output.strip())


def run_cli_from_base_path():
    with cd(base_dir):
        output = subprocess.check_output(['dotnet', 'run', '--project', 'Cli'])
        assert_equal('Cli/appsettings.json', output.strip())


def run_cli_test_in_own_path():
    with cd(cli_test_dir):
        subprocess.check_call(['dotnet', 'test', '--no-build'])


def run_cli_test_from_base_path():
    with cd(base_dir):
        subprocess.check_call(['dotnet', 'test', '--no-build', 'Cli.Test/Cli.Test.csproj'])


def run_web_test_in_own_path():
    with cd(web_test_dir):
        subprocess.check_call(['dotnet', 'test', '--no-build'])


def run_web_test_from_base_path():
    with cd(base_dir):
        subprocess.check_call(['dotnet', 'test', '--no-build', 'Web.Test/Web.Test.csproj'])


def assert_equal(expected, actual):
    assert expected == actual, 'Incorrect output\nexpect: <{}>\nactual: <{}>'.format(expected, actual)


class cd:
    """Context manager for changing the current working directory"""
    def __init__(self, target_path):
        self.target_path = os.path.expanduser(target_path)

    def __enter__(self):
        self.original_path = os.getcwd()
        os.chdir(self.target_path)
        print('> {}'.format(self.target_path))

    def __exit__(self, exc_type, exc_val, exc_tb):
        os.chdir(self.original_path)


if __name__ == '__main__':
    main()
