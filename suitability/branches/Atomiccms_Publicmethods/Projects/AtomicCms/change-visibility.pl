#!/usr/bin/perl

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
		visibilityState($_);
		print F "$_$lf";
	}
	close(F);
}

sub visibilityState($)
{
	shift;
	if( /^ {8}[^ {}#\/[][^(]+$/ )
	{
		if( ! /(public|internal)/ )
		{
			if( /private/ )
			{
				s/private/internal/;
			}
			else
			{
				s/^( {8})/\1internal /;
			}
		}
	}
	$_;
}

use File::Find;
find(
	{
		wanted=>\&process,
		follow=>1
	},
	Cwd::getcwd()
);
