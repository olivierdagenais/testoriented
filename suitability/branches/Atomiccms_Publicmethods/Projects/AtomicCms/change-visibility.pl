#!/usr/bin/perl

my $regex;

sub process()
{
	my $file = $File::Find::name;
	return if $file !~ /\.cs$/i;

	open(F, $file);
	my @lines = <F>;
	close(F);

	open(F, ">$file");
	foreach $_ (@lines)
	{
		/(^|[^\r\n])([\r\n]*)$/;
		my $lf = $2;

		s/(\r|\n)+//g;

		if( /$regex/ )
		{
			if( ! /(public|internal)/ )
			{
				if( /private/ )
				{
					s/private/internal/;
				}
				elsif( /protected/ and not /internal/ )
				{
					s/(protected)/\1 internal/;
				}
				else
				{
					s/^( {8})/\1internal /;
				}
			}
		}

		print F "$_$lf";
	}
	close(F);
}

if( $ARGV[0] eq "visibilityState" )
{
	$regex = "^ {8}[^ {}#\/[][^(]+\$";
}
elsif( $ARGV[0] eq "visibility" )
{
	$regex = "^ {8}[^ {}#\/[].+\$";
}
else
{
	die;
}

use File::Find;
find(
	{
		wanted=>\&process,
		follow=>1
	},
	Cwd::getcwd()
);

