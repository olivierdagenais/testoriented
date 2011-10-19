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

		if( 
			(
				$ARGV[0] eq "visibilityState" 
				&& /^ {8}[^ {}#\/[][^(]+$/
			)
			||
			(
				$ARGV[0] eq "visibility" 
				&& /^ {8}[^ {}#\/[].+$/
			)
		)
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

$ARGV[0] eq "visibility" or $ARGV[0] eq "visibilityState" or die;

use File::Find;
find(
	{
		wanted=>\&process,
		follow=>1
	},
	Cwd::getcwd()
);

