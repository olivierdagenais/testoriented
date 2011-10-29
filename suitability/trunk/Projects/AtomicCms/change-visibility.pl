#!/usr/bin/perl

sub process()
{
	my $file = $File::Find::name;
	# Ignore the file if it's not a C# class
	return if $file !~ /\.cs$/i;

	# The original file will get rewritten, so read it all in memory
	open(F, $file);
	my @lines = <F>;
	close(F);

	# Open the original file for write
	open(F, ">$file");
	foreach $_ (@lines)
	{
		# Save the line endings
		/(^|[^\r\n])([\r\n]*)$/;
		my $lf = $2;

		# Strip the line endings
		s/(\r|\n)+//g;

		# Lines we're interested in:
		# - are indented with 8 spaces
		# - don't start with
		#   - curly braces (block start or end)
		#   - '#' (#region and #endregion)
		#   - '[' (attributes)
		# We then distinguish between fields, properties and methods:
		# - methods contain an opening parenthesis on the line
		# - fields and properties don't contain a parenthesis on their line
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
			# If the element is already public or internal, proceed to the next line
			if( ! /(public|internal)/ )
			{
				if( /private/ )
				{
					# Replace "private" by "internal"
					s/private/internal/;
				}
				elsif( /protected/ and not /internal/ )
				{
					# Replace "protected" by "protected internal"
					s/(protected)/\1 internal/;
				}
				else
				{
					# Prefix elements with default visibility with "internal"
					s/^( {8})/\1internal /;
				}
			}
		}

		# Write out the altered line with its original line feeds
		print F "$_$lf";
	}
	close(F);
}

if( $ARGV[0] ne "visibility" and $ARGV[0] ne "visibilityState" )
{
	die <<HELP;

Rewrites C# classes to increate their visibility, processing
everything recursively found under the current working directory.

Filtering is done as follows:
 - "private" becomes "internal"
 - "protected" becomes "protected internal"

One of these two filtering levels must be specified at the command-line:
 - visibilityState: fields and properties
 - visibility: fields, properties and methods

HELP
}

# Execute the "process" function on every file found under the current working directory
use File::Find;
find(
	{
		wanted=>\&process,
		follow=>1
	},
	Cwd::getcwd()
);

