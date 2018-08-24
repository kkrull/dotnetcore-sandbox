#!/usr/bin/env python2.7

import os
import subprocess

base_dir = os.path.dirname(os.path.realpath(__file__))
cli_dir = os.path.join(base_dir, 'Cli')


def main():
    run_cli_from_cli()
    run_cli_from_base()


def run_cli_from_cli():
    with cd(cli_dir):
        output = subprocess.check_output(['dotnet', 'run'])
        assert_equal('Cli/appsettings.json', output.strip())


def run_cli_from_base():
    with cd(base_dir):
        output = subprocess.check_output(['dotnet', 'run', '--project', 'Cli'])
        assert_equal('Cli/appsettings.json', output.strip())


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
        print('< {}'.format(self.target_path))


if __name__ == '__main__':
    main()
